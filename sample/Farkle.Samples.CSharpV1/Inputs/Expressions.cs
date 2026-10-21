// C# 1.0 style source used as a parser sample. This file is not compiled.
using System;

namespace Sample.Expressions
{
    public class Expressions
    {
        public int All(int a, int b, object o, int[] values)
        {
            int x = a + b * 2 - 3 / 4 % 5;
            x = a << 2 | b >> 1 & 7 ^ 3;
            bool q = a < b && b <= a || a == b != false;
            x = q ? a : b;
            x += 1; x -= 1; x *= 2; x /= 2; x %= 3;
            x &= 1; x |= 1; x ^= 1; x <<= 1; x >>= 1;

            x = (int)o;
            x = (Foo)o;
            x = (Foo.Bar)o;
            x = (int[])o;
            x = (int*)o;
            x = (void*)o;
            x = (a) - b;
            x = (a)(b);
            (a)++;
            x = (a)!q;
            x = (a)~x;
            x = a[0];
            x = values[0, 1];
            x = (new int[5])[0];
            x = (a.b)[0];
            x = (a.b).c;
            x = (a.b) as object;
            q = o is Expressions;

            object t = typeof(void);
            x = sizeof(int);
            x = default(int);
            x = checked(a + b);
            x = unchecked(a * b);

            Foo foo = new Foo();
            Foo.Bar bar = new Foo.Bar();
            int[] array = new int[5];
            int[,] matrix = new int[2, 3];
            int[][] jagged = new int[2][];
            int[] init = new int[] { 1, 2, 3 };
            int[] init2 = { 4, 5, 6 };
            return x;
        }

        public void Calls(ref int x, out int y)
        {
            y = 0;
            M(ref x, out y);
            M(1, 2);
            this.M(1, 2);
            base.ToString();

            string s = "text \"escaped\" \n \x41 A";
            string v = @"verbatim ""quoted""
multi-line";
            char c = '\n';
            int hex = 0xFF;
            uint u = 10U;
            long l = 10L;
            ulong ul = 10UL;
            float f = 1.5F;
            double d = 1.5e10;
            decimal m = 1.5M;
        }

        void M(int a, int b) { }
    }

    class Foo
    {
        public class Bar { }
    }
}
