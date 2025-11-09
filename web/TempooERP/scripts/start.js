// Cross-platform start script for Angular dev server with PORT fallback
const { spawn } = require('node:child_process');

const rawPort = process.env.PORT;
const port = /^\d+$/.test(String(rawPort || '')) ? String(rawPort) : '4200';

const args = ['serve', '--proxy-config', 'proxy.conf.cjs', '--port', port];

const child = spawn('ng', args, {
  stdio: 'inherit',
  shell: true,
});

child.on('exit', (code) => process.exit(code ?? 0));
