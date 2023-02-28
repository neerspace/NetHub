import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import ssl from 'vite-plugin-mkcert'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), ssl()],
  server: {
    host: true,
    https: true
  }
})