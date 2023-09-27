/** @type {import('tailwindcss').Config} */
const colors = require("tailwindcss/colors");
const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
  //  mode: 'jit',
  //    purge: [
  //  "*.razor",
  //  "./{Pages,Shared,Components}/**/*.{html,js,cshtml,razor}",
  //  "./Shared/**/*.{html,js,cshtml,razor}",
  //  "../PureBlazor.Client/**/*.{html,js,razor}"
  //],
  content: [
    "*.razor",
    "./{Pages,Shared,Components}/**/*.{html,js,cshtml,razor}",
  ],
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/typography'),
  ],
  theme: {
    extend: {
        fontFamily: {
            sans: ['Inter var', ...defaultTheme.fontFamily.sans],
        },
    },
    colors: {
      brand: {
        '50': '#eff9ff',
        '100': '#dff1ff',
        '200': '#b7e5ff',
        '300': '#77d1ff',
        '400': '#2fbbff',
        '500': '#04a3f3',
        '600': '#0081d0',
        '700': '#0067a8',
        '800': '#015486',
        '900': '#074873',
        '950': '#052e4c',
    },
      ...colors
    },
  },
};
