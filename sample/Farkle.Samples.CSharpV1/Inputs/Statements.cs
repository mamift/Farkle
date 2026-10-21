// C# 1.0 style source used as a parser sample. This file is not compiled.
using System;

namespace Sample.Statements
{
    public class Flow
    {
        public void All(int n, object o)
        {
            int i = 0;
            int[] values = new int[10];

            if (n > 0) { i++; }
            else if (n < 0) { i--; }
            else { i = 0; }

            while (i < n) { i++; }
            do { i--; } while (i > 0);

            for (int j = 0; j < n; j++) { values[j] = j; }
            for (i = 0, n = 0; i < 10; i++) { }
            foreach (int v in values) { i += v; }

            switch (n)
            {
                case 0:
                case 1:
                    i++;
                    break;
                default:
                    i--;
                    break;
            }

            try { i++; }
            catch (Exception e) { i = -1; }
            catch { i = -2; }
            finally { i = 0; }

            checked { i++; }
            unchecked { i--; }

            lock (this) { i++; }
            using (Disposable d = new Disposable()) { i++; }

            unsafe
            {
                int* p = null;
                fixed (int* q = p) { *q = 1; }
            }

            goto done;
        label:
            i++;
        done:
            return;
        }

        public int Thrower() { throw new Exception(); }
        public void Rethrow() { throw; }
        public void Empty() { ; }
    }
}
