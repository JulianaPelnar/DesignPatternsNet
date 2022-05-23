// See https://aka.ms/new-console-template for more information
using DesignPatterns.Builder;
using DesignPatterns.Builder.FacetedBuilder;
using DesignPatterns.Builder.FunctionalBuilder;
using DesignPatterns.Factory;
using DesignPatterns.Factory.OCP;
using DesignPatterns.Factory.InnerFactory;
using DesignPatterns.SolidPrinciples;
using DesignPatterns.Prototype;
using DesignPatterns.Prototype.CopyConstructors;
using DesignPatterns.Prototype.EDCI;
using DesignPatterns.Prototype.PrototypeInheritance;
using DesignPatterns.Prototype.CTS;

Console.WriteLine("Hello, World!");

//object p = new SingleResponsibilityPrinciple();
//object ocp = new Open_ClosedPrinciple();
//object lsp = new LiskovSubstitutionPrinciple();
//object dip = new DependencyInversionPrinciple();
//object lwb = new LifeWithoutBuilder();
//object b = new Builder();
//object bf = new FluentBuilder();
//object bfg = new FluentBuilderInheritanceRecursiveGenerics();
//object sw = new StepwiseBuilder();
//object fb = new FunctionalBuilder();
//object fab = new FacetedBuilder();
//object fm = new FactoryMethod();
//object otbr = new ObjectTrackingAndBulkReplacement();
//object inf = new InnerFactory();
//object af = new AbstractFactory();
//object afOCP = new AbstractFactoryOCP();
//object icib = new ICloneableIsBad();
//object cc = new CopyConstructors();
//object edci = new ExplicitDeepCopyInterface();
//object pi = new PrototypeInheritance();
object cts = new CopyThroughSerialization();
