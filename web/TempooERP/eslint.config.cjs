const js = require('@eslint/js');
const tsParser = require('@typescript-eslint/parser');
const tsPlugin = require('@typescript-eslint/eslint-plugin');
const angular = require('@angular-eslint/eslint-plugin');
const angularTemplate = require('@angular-eslint/eslint-plugin-template');

module.exports = [
  // Ignorar build / tooling
  {
    ignores: [
      'dist/**',
      'node_modules/**',
      'coverage/**',
      '.angular/**',
      'tmp/**',
      'temp/**',
    ],
  },

  // ============================
  //  TypeScript + Angular (TS)
  // ============================
  {
    files: ['**/*.ts'],
    languageOptions: {
      parser: tsParser,
      parserOptions: {
        project: ['./tsconfig.json'],
        tsconfigRootDir: __dirname,
        sourceType: 'module',
        ecmaVersion: 'latest',
      },
    },
    plugins: {
      '@typescript-eslint': tsPlugin,
      '@angular-eslint': angular,
    },
    rules: {
      // Base JS
      ...js.configs.recommended.rules,

      // TS recomendado
      ...tsPlugin.configs.recommended.rules,

      // Angular recomendado (TS)
      ...angular.configs.recommended.rules,

      // Ajustes prácticos TempooERP
      '@typescript-eslint/no-unused-vars': [
        'warn',
        { argsIgnorePattern: '^_', varsIgnorePattern: '^_' },
      ],
      '@typescript-eslint/no-explicit-any': 'off', // súbelo a 'warn' cuando quieras apretar
      '@angular-eslint/component-class-suffix': [
        'error',
        { suffixes: ['Component', 'Page', 'Shell'] },
      ],
      '@angular-eslint/directive-class-suffix': [
        'error',
        { suffixes: ['Directive'] },
      ],
      '@angular-eslint/no-empty-lifecycle-method': 'warn',
    },
  },

  // ============================
  //  Templates Angular (HTML)
  // ============================
  {
    files: ['**/*.html'],
    languageOptions: {
      parser: angularTemplate.parser,
    },
    plugins: {
      '@angular-eslint/template': angularTemplate,
    },
    rules: {
      // Reglas recomendadas para templates
      ...angularTemplate.configs.recommended.rules,

      '@angular-eslint/template/no-negated-async': 'off',
    },
  },
];