// C# 1.0 style source used as a parser sample. This file is not compiled.
using System;

namespace Sample.Unsafe
{
    public unsafe class Pointers
    {
        public int Size;

        public void Run(int[] data)
        {
            fixed (int* p = data)
            {
                int* q = p;
                *q = 1;
                q++;
                int v = *q;
                void* raw = q;
                int n = sizeof(int*);
            }

            int* allocated = stackalloc int[10];
            allocated[0] = 1;

            Pointers* self = null;
            int size = self->Size;
        }
    }

    unsafe struct Buffer
    {
        public fixed byte Data[16];
    }
}
