// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Contracts;

namespace ContactBook;

public class Program
{
    public static void Main()
    {

      var cb = new ContactBook(ContactSeed.Contacts);
      cb.Start();

    }
}
