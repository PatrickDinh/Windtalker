using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Windtalker.Plumbing.Logging
{
    public class JObjectDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value,
                                   ILogEventPropertyValueFactory propertyValueFactory,
                                   out LogEventPropertyValue result)
        {
            result = null;
            var wrapper = value as WrappedJObject;
            if (wrapper == null)
            {
                return false;
            }
            var data = wrapper.Value as JObject;
            if (data == null)
            {
                return false;
            }
            var values = ReadProperties(data);
            result = new StructureValue(values);
            return true;
        }

        private IEnumerable<LogEventProperty> ReadProperties(JObject data)
        {
            foreach (var token in data)
            {
                yield return ReadProperty(token);
            }
        }

        private LogEventProperty ReadProperty(KeyValuePair<string, JToken> token)
        {
            return new LogEventProperty(token.Key, ReadValue(token.Value));
        }

        private LogEventPropertyValue ReadValue(JToken token)
        {
            var array = token as JArray;
            if (array != null)
            {
                return new SequenceValue(array.Children().Select(ReadValue));
            }
            var ctor = token as JConstructor;
            if (ctor != null)
            {
                throw new NotSupportedException("JConstructor tokens aren't supported yet.");
            }
            var @object = token as JObject;
            if (@object != null)
            {
                return new StructureValue(ReadProperties(@object));
            }
            var property = token as JProperty;
            if (property != null)
            {
                throw new NotSupportedException("JProperty tokens aren't supported yet.");
            }
            var value = token as JValue; //JRaw inherits JValue too
            if (value != null)
            {
                return new ScalarValue(value.Value);
            }
            throw new NotSupportedException("Token not supported: " + token);
        }
    }
}