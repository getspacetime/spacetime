const MonacoWebpackPlugin = require('monaco-editor-webpack-plugin');
const path = require('path');

module.exports = {
	mode: 'development',
	entry: {
		index: './index.js',
		editor: './editor.js'
    },
    output: {
        path: path.resolve(__dirname, 'dist'),
		filename: '[name].bundle.js',
    },
	module: {
		rules: [
			{
				test: /\.css$/,
				use: ['style-loader', 'css-loader', 'postcss-loader']
			},
			{
				test: /\.ttf$/,
				use: ['file-loader']
			}

		]
	},
	plugins: [
		new MonacoWebpackPlugin(),
	]
};