using System;
namespace DesignPatterns.SolidPrinciples
{
    // Interface segregation

	public class Document
    {

    }

	public interface IMachine
    {
		void Print(Document d);
		void Scan(Document d);
		void Fax(Document d);
	}

    // A single interface with a lot of methods
    public class MultifunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    // Interface forces to implement methods even though they are not needed
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    // Then with Interface Segregation

    public interface IPrinter
    {
        public void Print(Document d);
    }

    public interface IScanner
    {
        public void Scanner(Document d);
    }

    // And to make a photocopier
    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scanner(Document d)
        {
            throw new NotImplementedException();
        }
    }

    // Multifunction interface example
    public interface IMultiFunctionDevice : IPrinter, IScanner
    {

    }

    public class MultiFunctionDevice : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionDevice(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
                throw new ArgumentNullException(paramName: nameof(printer));
            if (scanner == null)
                throw new ArgumentNullException(paramName: nameof(scanner));
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scanner(Document d)
        {
            scanner.Scanner(d);
        }
        // decorator pattern
    }

    public class InterfaceSegregationPrinciple
	{
		public InterfaceSegregationPrinciple()
		{
		}
	}
}

