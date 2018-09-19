using System;

namespace Checkers {
    public static class CharExtention { // this class add the methods below to char primitives

        public static char ToLower(this char input) {

            string temp = input.ToString().ToLower();
            return temp[0];

        }

        public static char ToUpper(this char input) {

            string temp = input.ToString().ToUpper();
            return temp[0];

        }

        public static int ToInteger(this char input) {

            int returnValue = 0;
            if(!int.TryParse(input.ToString(),out returnValue))
                throw new FormatException("Char Cannot Be Parsed To An Integer Value");
            return returnValue;

        }

    }
}