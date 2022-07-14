const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { EnvironmentPlugin } = require('webpack');


module.exports = () => {

  return {
  entry: './src/index.tsx',

  devServer: {
    allowedHosts: 'all',
    hot: true,
    
    historyApiFallback: true,
    port: 8888,
    open: true,


    headers: {
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
      "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
    },
  },

  mode: 'development',
  devtool: 'inline-source-map',
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },

      {
        test: /\.s[ac]ss$/i,
        use: [
          "style-loader",
          "css-loader",
          "sass-loader",
        ],
      },
      
      {
        test: /\.(png|jpg|jpeg|gif)$/i,
        type: "asset/resource",
      },

    ],
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },

  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'dist'),
    clean: true,
  },

  plugins: [
    new HtmlWebpackPlugin({
      title: 'Development',
      template: './src/index.html',
    }),

    new EnvironmentPlugin({
      'process.env.REACT_APP_API_URL': process.env.REACT_APP_API_URL != null
      ? process.env.REACT_APP_API_URL : 'localhost:80',
      REACT_APP_API_URL: process.env.REACT_APP_API_URL != null
      ? process.env.REACT_APP_API_URL : 'localhost:80',
    })
  ],
}
};