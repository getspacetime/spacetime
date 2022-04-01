module.exports = {
    content: ["*.{html,js}", "../Pages/*.razor", "../Shared/**/*.razor"],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography'),
    ],
}