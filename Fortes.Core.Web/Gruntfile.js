module.exports = function (grunt) {
	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),

		// Copy web assets from wwwroot/lib to more convenient directories.
		copy: {
			main: {
				files: [
					// Vendor scripts.
					{
						expand: true,
						cwd: 'wwwroot/lib/bootstrap-sass/assets/javascripts/',
						src: ['**/*.js'],
						dest: 'wwwroot/scripts/bootstrap-sass/'
					},
					{
						expand: true,
						cwd: 'wwwroot/lib/jquery/dist/',
						src: ['**/*.js', '**/*.map'],
						dest: 'wwwroot/scripts/jquery/'
					},

					// Fonts.
					{
						expand: true,
						filter: 'isFile',
						flatten: true,
						cwd: 'wwwroot/lib/',
						src: ['bootstrap-sass/assets/fonts/**'],
						dest: 'wwwroot/fonts/bootstrap'
					},

					// Stylesheets
					{
						expand: true,
						cwd: 'wwwroot/lib/bootstrap-sass/assets/stylesheets/',
						src: ['**/*.scss'],
						dest: 'wwwroot/scss/'
					}
				]
			},
		},

		// Compile SASS files into minified CSS.
		sass: {
			options: {
				includePaths: ['wwwroot/lib/bootstrap-sass/assets/stylesheets']
			},
			dist: {
				options: {
					outputStyle: 'compressed'
				},
				files: {
					'wwwroot/css/app.css': 'wwwroot/scss/app.scss'
				}
			}
		},

		// Watch these files and notify of changes.
		watch: {
			grunt: { files: ['Gruntfile.js'] },

			sass: {
				files: [
					'scss/**/*.scss'
				],
				tasks: ['sass']
			}
		}
	});

	// Load externally defined tasks. 
	grunt.loadNpmTasks('grunt-sass');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-copy');

	// Establish tasks we can run from the terminal.
	grunt.registerTask('build', ['sass', 'copy']);
	grunt.registerTask('default', ['build', 'watch']);
}