module.exports = {
  devServer: {
    proxy: {
      '^/title': {
        target: 'https://art-explorer.kyma.fun',
      },
      '^/art': {
        target: 'https://art-explorer.kyma.fun'
      },
      '^/likes': {
        target: 'https://art-explorer.kyma.fun'
      }
    }
  }
}