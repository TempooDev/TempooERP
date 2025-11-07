const API_URL = process.env.API_URL || "http://localhost:5000";

module.exports = {
  "/api": {
    target: API_URL,
    secure: false,
    changeOrigin: true,
    logLevel: "info"
  }
};
