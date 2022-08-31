const { defineConfig } = require('@vue/cli-service')

let publicPath = "/";

if (process.env.NODE_ENV === 'production') {
  publicPath = "/spo"
} 


module.exports = defineConfig({
  publicPath,
  transpileDependencies: true,
  css:{
    loaderOptions:{
      sass:{
        additionalData: `@import "@/assets/_shared.scss";`
      }
    }
  }
})
