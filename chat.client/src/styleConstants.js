const { createTheme } = require("@mui/material");
const tColors = require("tailwindcss/colors");

class Style {
  static colors = {
    primary: "#32a854",
    primaryHover: "#32a87d",
    darkPrimary: "#308c4a",
    slate: tColors.slate,
    red: tColors.red,
    white: tColors.white,
    blue: tColors.blue,
    aqua: tColors.aqua,
    black: tColors.black,
    gray: tColors.gray,
    dark: "#18191a",
    darkHover: "#3c3e40",
    yellow: tColors.yellow,
    green: tColors.green,
    dimmed: "#ebe8e1",
    lime: tColors.lime,
  };
  static theme = createTheme({
    typography: {
      fontFamily: "Talldark",
    },
    components: {
      MuiCssBaseline: {
        styleOverrides: `
          @font-face {
            font-family: "Talldark";
            src: url("../public/fonts/talldark.ttf"), format('ttf');
          }      
        `,
      },
    },
  });
}

export default Style;