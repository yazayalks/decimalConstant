using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecimalConstant
{
    public enum TypeLexemes // Типы лексем
    {
        textOrIdentifier, // текст или переменная
        identifier, // переменная
        text, // текст 
        function, // printf
        errorAndFunction, // printf
        number, // число
        quote,// кавычка
        comma, // запятая
        leftBracket, // левая скобка
        rightBracket, // правая скобка
        reference, // ссылка на переменную
        endString, // конец строки
        semicolon, // точка с запятой
        error, // ошибка
        space, // пробел
        symbol, // символ
        secondQuote, // вторая кавычка
        plus, // плюс
        minus, // минус
        power // степень

    }

    public struct Lexemes
    {
        public int start;
        public string value;
        public TypeLexemes type;

        public Lexemes(int start, string value, TypeLexemes type)
        {
            this.start = start;
            this.value = value;
            this.type = type;
        }
    }

    public static class CodeScanner
    {
        public static Lexemes FindNumber(string s, int i)
        {
            if (i >= s.Length)
            {
                return new Lexemes(i, "", TypeLexemes.endString);
            }
            while (s[i] == '\t' || s[i] == ' ' || s[i] == '\n')
            {
                i++;
                if (i >= s.Length)
                    return new Lexemes(i, "", TypeLexemes.endString);
            }
            if ((!Char.IsDigit(s[i])) && (s[i] != '+') && (s[i] != '-') && (s[i] != ',') && (s[i] != 'E') && (s[i] != 'e'))
            {
                int j = i;
                while ((!Char.IsDigit(s[j])) && (s[j] != '+') && (s[j] != '-') && (s[j] != ',') && (s[j] != 'E') && (s[j] != 'e'))
                {
                    j++;
                    if (j >= s.Length)
                        break;
                }
                return new Lexemes(i, s.Substring(i, j - i), TypeLexemes.error);
            }
            if (Char.IsDigit(s[i]))
            {
                int j = i;
                while (Char.IsDigit(s[j]))
                {
                    j++;
                    if (j >= s.Length)
                        break;
                }
                return new Lexemes(i, s.Substring(i, j - i), TypeLexemes.number);
            }
            if ((i < s.Length) && (s[i] == '+'))
            {
                return new Lexemes(i, "+", TypeLexemes.plus);
            }
            if ((i < s.Length) && (s[i] == '-'))
            {
                return new Lexemes(i, "-", TypeLexemes.minus);
            }
            if ((i < s.Length) && (s[i] == ','))
            {
                return new Lexemes(i, ",", TypeLexemes.comma);
            }
            if ((i < s.Length) && ((s[i] == 'E') || (s[i] == 'e')))
            {
                return new Lexemes(i, "E", TypeLexemes.power);
            }

            return new Lexemes(i, s[i].ToString(), TypeLexemes.error);
        }
    }
}


