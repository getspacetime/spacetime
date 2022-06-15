module.exports = {
    darkMode: 'class',
    content: ["*.{html,js}", "../Pages/*.razor", "../Shared/**/*.razor", "../../Spacetime.Blazor.*/Components/**/*.razor"],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography'),
    ],
}