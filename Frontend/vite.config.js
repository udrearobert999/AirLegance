import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      Components: path.resolve(__dirname, 'src/Components'),
      Hooks: path.resolve(__dirname, 'src/Hooks'),
      Pages: path.resolve(__dirname, 'src/Pages'),
      Api: path.resolve(__dirname, 'src/Api'),
      Contexts: path.resolve(__dirname, 'src/Contexts'),
    },
  },
  server: {
    port: 3000,
  },
});
