const js = require('@eslint/js');
const tsParser = require('@typescript-eslint/parser');
const tsPlugin = require('@typescript-eslint/eslint-plugin');
const angular = require('@angular-eslint/eslint-plugin');
const angularTemplate = require('@angular-eslint/eslint-plugin-template');
const angularTemplateParser = require('@angular-eslint/template-parser');

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
        // Do not use project-based type-checking here to avoid parser errors; keep non-type-aware rules
        sourceType: 'module',
        ecmaVersion: 'latest',
      },
      globals: {
        // Browser globals for application code
        window: 'readonly',
        document: 'readonly',
        console: 'readonly'
      }
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
      parser: angularTemplateParser,
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

  // ============================
  //  Node (SSR / server files)
  // ============================
  {
    files: ['src/server.ts', '**/*.server.ts'],
    languageOptions: {
      parser: tsParser,
      parserOptions: { sourceType: 'module', ecmaVersion: 'latest' },
      globals: {
        process: 'readonly',
        console: 'readonly',
        global: 'readonly'
      }
    },
    plugins: {
      '@typescript-eslint': tsPlugin,
      '@angular-eslint': angular,
    },
    rules: {
      ...js.configs.recommended.rules,
      ...tsPlugin.configs.recommended.rules,
      // Server-specific adjustments
      'no-undef': 'off'
    }
  },

  // ============================
  //  Test Specs (Jasmine)
  // ============================
  {
    files: ['**/*.spec.ts'],
    languageOptions: {
      parser: tsParser,
      parserOptions: { sourceType: 'module', ecmaVersion: 'latest' },
      globals: {
        describe: 'readonly',
        it: 'readonly',
        beforeEach: 'readonly',
        expect: 'readonly',
        HTMLElement: 'readonly',
        console: 'readonly'
      }
    },
    plugins: {
      '@typescript-eslint': tsPlugin,
      '@angular-eslint': angular,
    },
    rules: {
      ...js.configs.recommended.rules,
      ...tsPlugin.configs.recommended.rules,
      ...angular.configs.recommended.rules,
      'no-undef': 'off'
    }
  }
];