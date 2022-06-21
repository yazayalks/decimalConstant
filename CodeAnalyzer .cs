using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecimalConstant
{
    public struct SyntaxError
    {
        public int code;
        public string what;
        public int pos;

        public SyntaxError(int code, string what, int pos)
        {
            this.code = code;
            this.what = what;
            this.pos = pos;
        }
    }


    class CodeAnalyzer
    {
        int current = 0;     // индекс текущей лексемы
        List<SyntaxError> syntaxErrors = new List<SyntaxError>();
        List<Lexemes> lexemes = new List<Lexemes>();
        Lexemes l;
        string str;
        string strOutput;


        char[] charsToTrim = { ' ', '\n', '\t' };
        public CodeAnalyzer(string str)
        {
            this.str = str;

        }

        public List<SyntaxError> GetListSyntaxErrors()
        {

            current = 0;
            RunScan();
            return syntaxErrors;

        }
        public List<Lexemes> GetListLexems()
        {

            return lexemes;

        }
  
        private void RunScan()
        {
            int i = 0;
            while (i < str.Length) 
            {
                l = CodeScanner.FindNumber(str, i);
                lexemes.Add(l);
                i = lexemes[lexemes.Count - 1].start + lexemes[lexemes.Count - 1].value.Length;
            }
            A();
        }
        public void A()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  'E' или '+ | -' или 'number'";
                syntaxErrors.Add(new SyntaxError(1, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                C();
                return;
            }
            if (lexemes[current].type == TypeLexemes.comma)
            {
                current++;
                E();
                return;
            }
            if (lexemes[current].type == TypeLexemes.power)
            {
                current++;
                F();
                return;
            }
            if ((lexemes[current].type == TypeLexemes.plus) || (lexemes[current].type == TypeLexemes.minus))
            {
                current++;
                B();
                return;
            }
            if ((lexemes[current].type != TypeLexemes.number) && (lexemes[current].type != TypeLexemes.plus) && (lexemes[current].type != TypeLexemes.minus))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  'E' или '+ | -' или 'number'"; 
                syntaxErrors.Add(new SyntaxError(2, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                A();
                return;
            }
        }
        public void B()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  'E' или ',' или 'number'";
                syntaxErrors.Add(new SyntaxError(3, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.comma)
            {
                current++;
                E();
                return;
            }
            if (lexemes[current].type == TypeLexemes.power)
            {
                current++;
                F();
                return;
            }
            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                C();
                return;
            }
            if ((lexemes[current].type != TypeLexemes.comma) && (lexemes[current].type != TypeLexemes.power) && (lexemes[current].type != TypeLexemes.number))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  'E' или ',' или 'number'";
                syntaxErrors.Add(new SyntaxError(4, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                B();
                return;
            }
        }
        public void E()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась 'number'";
                syntaxErrors.Add(new SyntaxError(5, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                H();
                return;
            }

            if (lexemes[current].type != TypeLexemes.number)
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась 'number'";
                syntaxErrors.Add(new SyntaxError(6, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                E();
                return;
            }
        }
        public void H()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  'E' или 'number' или 'endString'";
                syntaxErrors.Add(new SyntaxError(7, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.power)
            {
                current++;
                F();
                return;
            }
            if (lexemes[current].type == TypeLexemes.endString)
            {
                
                return;
            }
            if (lexemes[current].type != TypeLexemes.power)
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  'E' или 'number' или 'endString'";
                syntaxErrors.Add(new SyntaxError(8, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                H();
                return;
            }
        }
        public void C()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  ',' или 'number' или 'endString' или 'E'";
                syntaxErrors.Add(new SyntaxError(9, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }

            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                D();
                return;
            }
            if (lexemes[current].type == TypeLexemes.power)
            {
                current++;
                F();
                return;
            }
            if (lexemes[current].type == TypeLexemes.comma)
            {
                current++;
                E();
                return;
            }
            if (lexemes[current].type == TypeLexemes.endString)
            {
                
                return;
            }
            if ((lexemes[current].type != TypeLexemes.power) && (lexemes[current].type != TypeLexemes.number) && (lexemes[current].type != TypeLexemes.comma))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  ',' или 'number' или 'endString' или 'E'";
                syntaxErrors.Add(new SyntaxError(10, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                C();
                return;
            }
        }
        public void D()
        {

            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  'E' или 'number' или 'endString'";
                syntaxErrors.Add(new SyntaxError(11, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
                
            }
            if (lexemes[current].type == TypeLexemes.power)
            {
                current++;
                F();
                return;
            }
            
            if ((lexemes[current].type != TypeLexemes.power) && (lexemes[current].type != TypeLexemes.number))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  'E' или 'number' или 'endString'";
                syntaxErrors.Add(new SyntaxError(12, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                D();
                return;
            }
        }
        public void F()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  '+ | -' или 'number'";
                syntaxErrors.Add(new SyntaxError(13, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                I();
                return;
            }
            if ((lexemes[current].type == TypeLexemes.plus) || (lexemes[current].type == TypeLexemes.minus))
            {
                current++;
                G();
                return;
            }
            if ((lexemes[current].type != TypeLexemes.number) && (lexemes[current].type != TypeLexemes.plus) && (lexemes[current].type != TypeLexemes.minus))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  '+ | -' или 'number'";
                syntaxErrors.Add(new SyntaxError(14, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                F();
                return;
            }
        }
        public void G()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась 'number'";
                syntaxErrors.Add(new SyntaxError(15, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
            if (lexemes[current].type == TypeLexemes.number)
            {
                current++;
                I();
                return;
            }
         
            if (lexemes[current].type != TypeLexemes.number)
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась 'number'";
            syntaxErrors.Add(new SyntaxError(16, strOutput, lexemes[current].start + lexemes[current].value.Length));
            current++;
                G();
            return;
        }

        public void I()
        {
            if (current >= lexemes.Count)
            {
                strOutput = "Встреченно " + "' " + "Неожиданный конец строки" + " '" + ", а ожидалась  'endString' или 'number'";
                syntaxErrors.Add(new SyntaxError(17, strOutput, lexemes[current - 1].start + lexemes[current - 1].value.Length));
                return;
            }
           
            if (lexemes[current].type == TypeLexemes.endString)
            {
                current++;
                return;
            }
             if((lexemes[current].type != TypeLexemes.endString) && (lexemes[current].type != TypeLexemes.number))
            {
                strOutput = "Встреченно " + "' " + lexemes[current].value + " '" + ", а ожидалась  'endString' или 'number'";
                syntaxErrors.Add(new SyntaxError(18, strOutput, lexemes[current].start + lexemes[current].value.Length));
                current++;
                I();
                return;
            }

        }
        
    }
   
}
