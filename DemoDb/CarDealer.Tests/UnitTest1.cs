//ReSharper disable InconsistentNaming, CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using CarDealer;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

[TestFixture]
public class Test_013_000_001
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        /*Mapper.Reset();
    Mapper.Initialize(cfg => cfg.AddProfile(GetType("ProductShopProfile")));*/
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<CarDealerProfile>();
        });

        this.serviceProvider = ConfigureServices<CarDealerContext>("CarDealer");
    }

    [Test]
    public void ImportSalesZeroTest()
    {
        var context = this.serviceProvider.GetService<CarDealerContext>();

        var salesJson =
            "[{\"carId\":105,\"customerId\":30,\"discount\":30},{\"carId\":234,\"customerId\":23,\"discount\":50},{\"carId\":342,\"customerId\":29,\"discount\":0},{\"carId\":329,\"customerId\":26,\"discount\":40},{\"carId\":235,\"customerId\":4,\"discount\":0},{\"carId\":213,\"customerId\":22,\"discount\":15},{\"carId\":14,\"customerId\":4,\"discount\":30},{\"carId\":26,\"customerId\":3,\"discount\":0},{\"carId\":34,\"customerId\":1,\"discount\":10},{\"carId\":332,\"customerId\":21,\"discount\":30},{\"carId\":170,\"customerId\":8,\"discount\":5},{\"carId\":2,\"customerId\":5,\"discount\":0},{\"carId\":166,\"customerId\":7,\"discount\":50},{\"carId\":301,\"customerId\":9,\"discount\":5},{\"carId\":204,\"customerId\":11,\"discount\":15},{\"carId\":302,\"customerId\":1,\"discount\":40},{\"carId\":173,\"customerId\":25,\"discount\":5},{\"carId\":146,\"customerId\":13,\"discount\":5},{\"carId\":210,\"customerId\":7,\"discount\":50},{\"carId\":206,\"customerId\":17,\"discount\":50},{\"carId\":135,\"customerId\":1,\"discount\":30},{\"carId\":220,\"customerId\":14,\"discount\":10},{\"carId\":358,\"customerId\":20,\"discount\":20},{\"carId\":199,\"customerId\":4,\"discount\":50},{\"carId\":201,\"customerId\":6,\"discount\":20},{\"carId\":238,\"customerId\":16,\"discount\":40},{\"carId\":171,\"customerId\":8,\"discount\":30},{\"carId\":169,\"customerId\":14,\"discount\":0},{\"carId\":336,\"customerId\":2,\"discount\":40},{\"carId\":56,\"customerId\":13,\"discount\":40},{\"carId\":13,\"customerId\":27,\"discount\":50},{\"carId\":62,\"customerId\":29,\"discount\":10},{\"carId\":93,\"customerId\":26,\"discount\":50},{\"carId\":71,\"customerId\":28,\"discount\":10},{\"carId\":252,\"customerId\":26,\"discount\":30},{\"carId\":55,\"customerId\":28,\"discount\":5},{\"carId\":60,\"customerId\":3,\"discount\":30},{\"carId\":249,\"customerId\":28,\"discount\":10},{\"carId\":151,\"customerId\":21,\"discount\":20},{\"carId\":279,\"customerId\":26,\"discount\":15},{\"carId\":307,\"customerId\":3,\"discount\":5},{\"carId\":198,\"customerId\":15,\"discount\":30},{\"carId\":231,\"customerId\":9,\"discount\":15},{\"carId\":212,\"customerId\":1,\"discount\":5},{\"carId\":79,\"customerId\":27,\"discount\":15},{\"carId\":121,\"customerId\":24,\"discount\":40},{\"carId\":243,\"customerId\":23,\"discount\":20},{\"carId\":186,\"customerId\":15,\"discount\":10},{\"carId\":49,\"customerId\":5,\"discount\":50},{\"carId\":165,\"customerId\":26,\"discount\":15},{\"carId\":176,\"customerId\":18,\"discount\":40},{\"carId\":61,\"customerId\":10,\"discount\":15},{\"carId\":322,\"customerId\":2,\"discount\":50},{\"carId\":24,\"customerId\":9,\"discount\":30},{\"carId\":58,\"customerId\":6,\"discount\":15},{\"carId\":264,\"customerId\":25,\"discount\":40},{\"carId\":159,\"customerId\":16,\"discount\":30},{\"carId\":97,\"customerId\":10,\"discount\":30},{\"carId\":147,\"customerId\":16,\"discount\":30},{\"carId\":39,\"customerId\":21,\"discount\":0},{\"carId\":164,\"customerId\":4,\"discount\":15},{\"carId\":25,\"customerId\":2,\"discount\":15},{\"carId\":335,\"customerId\":19,\"discount\":30},{\"carId\":58,\"customerId\":12,\"discount\":10},{\"carId\":272,\"customerId\":1,\"discount\":10},{\"carId\":161,\"customerId\":10,\"discount\":10},{\"carId\":247,\"customerId\":27,\"discount\":30},{\"carId\":50,\"customerId\":15,\"discount\":0},{\"carId\":184,\"customerId\":29,\"discount\":20},{\"carId\":17,\"customerId\":22,\"discount\":20},{\"carId\":156,\"customerId\":21,\"discount\":0},{\"carId\":41,\"customerId\":16,\"discount\":40},{\"carId\":209,\"customerId\":8,\"discount\":0},{\"carId\":160,\"customerId\":8,\"discount\":50},{\"carId\":275,\"customerId\":11,\"discount\":0},{\"carId\":191,\"customerId\":24,\"discount\":5},{\"carId\":68,\"customerId\":13,\"discount\":50},{\"carId\":326,\"customerId\":24,\"discount\":30},{\"carId\":75,\"customerId\":22,\"discount\":30},{\"carId\":88,\"customerId\":26,\"discount\":15},{\"carId\":167,\"customerId\":13,\"discount\":0},{\"carId\":16,\"customerId\":21,\"discount\":15},{\"carId\":154,\"customerId\":23,\"discount\":5},{\"carId\":320,\"customerId\":30,\"discount\":20},{\"carId\":109,\"customerId\":23,\"discount\":30},{\"carId\":219,\"customerId\":18,\"discount\":10},{\"carId\":48,\"customerId\":12,\"discount\":30},{\"carId\":112,\"customerId\":8,\"discount\":30}]";

        var actualOutput =
            StartUp.ImportSales(context, salesJson);

        var expectedOutput = "Successfully imported 88.";

        var assertContext = this.serviceProvider.GetService<CarDealerContext>();

        const int expectedCarsCount = 88;
        var actualCarsCount = assertContext.Sales.Count();

        Assert.That(actualCarsCount, Is.EqualTo(expectedCarsCount),
            $"Inserted {nameof(context.Sales)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(StartUp.ImportSales)} output is incorrect!");
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection()
           .AddDbContext<TContext>(t => t
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}