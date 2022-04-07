const path = require('path');
const CleanPlugin = require('clean-webpack-plugin');

module.exports = {
  mode: 'production',
  entry: './src/index.tsx',
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'dist')
  },
  devtool: 'none',
  module: {
    rules: [
      {
        test: /\.(ts | tsx)$/,
        use: 'ts-loader',
        exclude: /node_modules/
      }
    ]
  },
  resolve: {
    extensions: ['.ts', '.js', 'tsx']
  },
  plugins: [
    new CleanPlugin.CleanWebpackPlugin()
  ]
};