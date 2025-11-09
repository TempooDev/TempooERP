module.exports = {
  '/api': {
    target:
      process.env['services__erp-api__http__0'] ||
      process.env['services__erp-api__https__0'],
    secure: false,
    changeOrigin: true,
  },
};
