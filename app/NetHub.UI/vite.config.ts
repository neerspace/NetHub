import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import ssl from 'vite-plugin-mkcert'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), ssl()],
  server: {
    host: true,
    https: true,
    proxy: {
      '/api': {
        target: 'https://api.nethub.local:9010',
        secure: false,
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
    }
  }
})