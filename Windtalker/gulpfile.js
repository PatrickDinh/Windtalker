/// <binding AfterBuild='default' />
// include plug-ins
var gulp = require("gulp");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
var del = require("del");
var wiredep = require("wiredep").stream;
var ngHtml2Js = require("gulp-ng-html2js");
var flatten = require("gulp-flatten");

var paths = {
  build: "build",
  indexView: "Views/Home/index.html",
  index: "build/index.html",
  bower: {
    json: require("./bower.json"),
    directory: "bower_components"
  },
  js: ["app/**/*.js", "!app/**/*.min.js"],
  views: "app/**/*.tpl.html"
}

gulp.task("index", function () {
  return gulp.src(paths.indexView)
             .pipe(gulp.dest(paths.build));
})

gulp.task("clean", [], function (callback) {
  return del.sync([paths.build], callback);
});

gulp.task("scripts", [], function () {
  return gulp.src(paths.js)
             .pipe(concat("build.min.js"))
             .pipe(gulp.dest(paths.build));
});

gulp.task("views", [], function () {
  return gulp.src(paths.views)
             .pipe(flatten())
             .pipe(ngHtml2Js({ moduleName: "windtalker" }))
             .pipe(concat("views.js"))
             .pipe(gulp.dest(paths.build));
});

gulp.task("libraries", ["scripts", "index"], function () {
  var options = {
    bowerJson: paths.bower.json,
    directory: paths.bower.directory,
    src: paths.index
  };

  return gulp.src(paths.index)
             .pipe(wiredep(options))
             .pipe(gulp.dest(paths.build));
});

gulp.task("watch", [], function () {
  gulp.watch(paths.js, ["scripts"]);
  gulp.watch(paths.views, ["views"]);
});

//Set a default tasks
gulp.task("default", ["clean", "scripts", "views", "index", "libraries"], function () { });