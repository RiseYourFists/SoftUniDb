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
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

[TestFixture]
public class Test_013_000_001
{
    private IServiceProvider serviceProvider;

    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CarDealerProfile>();
        });

        this.serviceProvider = ConfigureServices<CarDealerContext>(Guid.NewGuid().ToString());
    }

    [Test]
    public void ImportSalesZeroTest()
    {
        var context = this.serviceProvider.GetService<CarDealerContext>();

        SeedDatabase(context);

        var salesXml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Sales><Sale><carId>105</carId><customerId>30</customerId><discount>30</discount></Sale><Sale><carId>234</carId><customerId>23</customerId><discount>50</discount></Sale><Sale><carId>342</carId><customerId>29</customerId><discount>0</discount></Sale><Sale><carId>329</carId><customerId>26</customerId><discount>40</discount></Sale><Sale><carId>235</carId><customerId>4</customerId><discount>0</discount></Sale><Sale><carId>213</carId><customerId>22</customerId><discount>15</discount></Sale><Sale><carId>14</carId><customerId>4</customerId><discount>30</discount></Sale><Sale><carId>26</carId><customerId>3</customerId><discount>0</discount></Sale><Sale><carId>34</carId><customerId>1</customerId><discount>10</discount></Sale><Sale><carId>332</carId><customerId>21</customerId><discount>30</discount></Sale><Sale><carId>170</carId><customerId>8</customerId><discount>5</discount></Sale><Sale><carId>2</carId><customerId>5</customerId><discount>0</discount></Sale><Sale><carId>166</carId><customerId>7</customerId><discount>50</discount></Sale><Sale><carId>301</carId><customerId>9</customerId><discount>5</discount></Sale><Sale><carId>204</carId><customerId>11</customerId><discount>15</discount></Sale><Sale><carId>302</carId><customerId>1</customerId><discount>40</discount></Sale><Sale><carId>173</carId><customerId>25</customerId><discount>5</discount></Sale><Sale><carId>146</carId><customerId>13</customerId><discount>5</discount></Sale><Sale><carId>210</carId><customerId>7</customerId><discount>50</discount></Sale><Sale><carId>206</carId><customerId>17</customerId><discount>50</discount></Sale><Sale><carId>135</carId><customerId>1</customerId><discount>30</discount></Sale><Sale><carId>220</carId><customerId>14</customerId><discount>10</discount></Sale><Sale><carId>358</carId><customerId>20</customerId><discount>20</discount></Sale><Sale><carId>199</carId><customerId>4</customerId><discount>50</discount></Sale><Sale><carId>201</carId><customerId>6</customerId><discount>20</discount></Sale><Sale><carId>238</carId><customerId>16</customerId><discount>40</discount></Sale><Sale><carId>171</carId><customerId>8</customerId><discount>30</discount></Sale><Sale><carId>169</carId><customerId>14</customerId><discount>0</discount></Sale><Sale><carId>336</carId><customerId>2</customerId><discount>40</discount></Sale><Sale><carId>56</carId><customerId>13</customerId><discount>40</discount></Sale><Sale><carId>13</carId><customerId>27</customerId><discount>50</discount></Sale><Sale><carId>62</carId><customerId>29</customerId><discount>10</discount></Sale><Sale><carId>93</carId><customerId>26</customerId><discount>50</discount></Sale><Sale><carId>71</carId><customerId>28</customerId><discount>10</discount></Sale><Sale><carId>252</carId><customerId>26</customerId><discount>30</discount></Sale><Sale><carId>55</carId><customerId>28</customerId><discount>5</discount></Sale><Sale><carId>60</carId><customerId>3</customerId><discount>30</discount></Sale><Sale><carId>249</carId><customerId>28</customerId><discount>10</discount></Sale><Sale><carId>151</carId><customerId>21</customerId><discount>20</discount></Sale><Sale><carId>279</carId><customerId>26</customerId><discount>15</discount></Sale><Sale><carId>307</carId><customerId>3</customerId><discount>5</discount></Sale><Sale><carId>198</carId><customerId>15</customerId><discount>30</discount></Sale><Sale><carId>231</carId><customerId>9</customerId><discount>15</discount></Sale><Sale><carId>212</carId><customerId>1</customerId><discount>5</discount></Sale><Sale><carId>79</carId><customerId>27</customerId><discount>15</discount></Sale><Sale><carId>121</carId><customerId>24</customerId><discount>40</discount></Sale><Sale><carId>243</carId><customerId>23</customerId><discount>20</discount></Sale><Sale><carId>186</carId><customerId>15</customerId><discount>10</discount></Sale><Sale><carId>49</carId><customerId>5</customerId><discount>50</discount></Sale><Sale><carId>165</carId><customerId>26</customerId><discount>15</discount></Sale><Sale><carId>176</carId><customerId>18</customerId><discount>40</discount></Sale><Sale><carId>61</carId><customerId>10</customerId><discount>15</discount></Sale><Sale><carId>322</carId><customerId>2</customerId><discount>50</discount></Sale><Sale><carId>24</carId><customerId>9</customerId><discount>30</discount></Sale><Sale><carId>58</carId><customerId>6</customerId><discount>15</discount></Sale><Sale><carId>264</carId><customerId>25</customerId><discount>40</discount></Sale><Sale><carId>159</carId><customerId>16</customerId><discount>30</discount></Sale><Sale><carId>97</carId><customerId>10</customerId><discount>30</discount></Sale><Sale><carId>147</carId><customerId>16</customerId><discount>30</discount></Sale><Sale><carId>39</carId><customerId>21</customerId><discount>0</discount></Sale><Sale><carId>164</carId><customerId>4</customerId><discount>15</discount></Sale><Sale><carId>25</carId><customerId>2</customerId><discount>15</discount></Sale><Sale><carId>335</carId><customerId>19</customerId><discount>30</discount></Sale><Sale><carId>58</carId><customerId>12</customerId><discount>10</discount></Sale><Sale><carId>272</carId><customerId>1</customerId><discount>10</discount></Sale><Sale><carId>161</carId><customerId>10</customerId><discount>10</discount></Sale><Sale><carId>247</carId><customerId>27</customerId><discount>30</discount></Sale><Sale><carId>50</carId><customerId>15</customerId><discount>0</discount></Sale><Sale><carId>184</carId><customerId>29</customerId><discount>20</discount></Sale><Sale><carId>17</carId><customerId>22</customerId><discount>20</discount></Sale><Sale><carId>156</carId><customerId>21</customerId><discount>0</discount></Sale><Sale><carId>41</carId><customerId>16</customerId><discount>40</discount></Sale><Sale><carId>209</carId><customerId>8</customerId><discount>0</discount></Sale><Sale><carId>160</carId><customerId>8</customerId><discount>50</discount></Sale><Sale><carId>275</carId><customerId>11</customerId><discount>0</discount></Sale><Sale><carId>191</carId><customerId>24</customerId><discount>5</discount></Sale><Sale><carId>68</carId><customerId>13</customerId><discount>50</discount></Sale><Sale><carId>326</carId><customerId>24</customerId><discount>30</discount></Sale><Sale><carId>75</carId><customerId>22</customerId><discount>30</discount></Sale><Sale><carId>88</carId><customerId>26</customerId><discount>15</discount></Sale><Sale><carId>167</carId><customerId>13</customerId><discount>0</discount></Sale><Sale><carId>16</carId><customerId>21</customerId><discount>15</discount></Sale><Sale><carId>154</carId><customerId>23</customerId><discount>5</discount></Sale><Sale><carId>320</carId><customerId>30</customerId><discount>20</discount></Sale><Sale><carId>109</carId><customerId>23</customerId><discount>30</discount></Sale><Sale><carId>219</carId><customerId>18</customerId><discount>10</discount></Sale><Sale><carId>48</carId><customerId>12</customerId><discount>30</discount></Sale><Sale><carId>112</carId><customerId>8</customerId><discount>30</discount></Sale></Sales>";

        var actualOutput =
            StartUp.ImportSales(context, salesXml);

        var expectedOutput = "Successfully imported 9";

        var assertContext = this.serviceProvider.GetService<CarDealerContext>();

        const int expectedCarsCount = 9;
        var actualCarsCount = assertContext.Sales.Count();

        Assert.That(actualCarsCount, Is.EqualTo(expectedCarsCount),
            $"Inserted {nameof(context.Sales)} count is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(StartUp.ImportSales)} output is incorrect!");
    }

    private static void SeedDatabase(CarDealerContext context)
    {
        var datasetsJson =
           "{\"Customer\":[{\"Id\":1,\"Name\":\"Emmitt Benally\",\"BirthDate\":\"1993-11-20T00:00:00\",\"IsYoungDriver\":true},{\"Id\":2,\"Name\":\"Donnetta Soliz\",\"BirthDate\":\"1963-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":3,\"Name\":\"Garret Capron\",\"BirthDate\":\"1975-07-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":4,\"Name\":\"Hipolito Lamoreaux\",\"BirthDate\":\"1982-08-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":5,\"Name\":\"Sylvie Mcelravy\",\"BirthDate\":\"1983-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":6,\"Name\":\"Jimmy Grossi\",\"BirthDate\":\"1986-07-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":7,\"Name\":\"Faustina Burgher\",\"BirthDate\":\"1994-06-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":8,\"Name\":\"Daniele Zarate\",\"BirthDate\":\"1995-10-05T00:00:00\",\"IsYoungDriver\":false},{\"Id\":9,\"Name\":\"Cinthia Lasala\",\"BirthDate\":\"1992-11-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":10,\"Name\":\"Nisha Markwell\",\"BirthDate\":\"1994-04-04T00:00:00\",\"IsYoungDriver\":false},{\"Id\":11,\"Name\":\"Yvonne Mccalla\",\"BirthDate\":\"1992-03-02T00:00:00\",\"IsYoungDriver\":true},{\"Id\":12,\"Name\":\"Taina Achenbach\",\"BirthDate\":\"1994-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":13,\"Name\":\"Rema Revelle\",\"BirthDate\":\"1970-05-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":14,\"Name\":\"Carri Knapik\",\"BirthDate\":\"1972-02-02T00:00:00\",\"IsYoungDriver\":true},{\"Id\":15,\"Name\":\"Kristian Engberg\",\"BirthDate\":\"1981-10-03T00:00:00\",\"IsYoungDriver\":false},{\"Id\":16,\"Name\":\"Brett Brickley\",\"BirthDate\":\"1980-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":17,\"Name\":\"Oren Perlman\",\"BirthDate\":\"1988-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":18,\"Name\":\"Carole Witman\",\"BirthDate\":\"1987-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":19,\"Name\":\"Francis Mckim\",\"BirthDate\":\"1977-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":20,\"Name\":\"Audrea Cardinal\",\"BirthDate\":\"1976-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":21,\"Name\":\"Johnette Derryberry\",\"BirthDate\":\"1995-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":22,\"Name\":\"Teddy Hobby\",\"BirthDate\":\"1975-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":23,\"Name\":\"Rico Peer\",\"BirthDate\":\"1984-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":24,\"Name\":\"Lino Subia\",\"BirthDate\":\"1985-12-21T00:00:00\",\"IsYoungDriver\":false},{\"Id\":25,\"Name\":\"Hai Everton\",\"BirthDate\":\"1985-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":26,\"Name\":\"Zada Attwood\",\"BirthDate\":\"1982-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":27,\"Name\":\"Marcelle Griego\",\"BirthDate\":\"1990-10-04T00:00:00\",\"IsYoungDriver\":true},{\"Id\":28,\"Name\":\"Natalie Poli\",\"BirthDate\":\"1990-10-04T00:00:00\",\"IsYoungDriver\":false},{\"Id\":29,\"Name\":\"Louann Holzworth\",\"BirthDate\":\"1960-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":30,\"Name\":\"Ann Mcenaney\",\"BirthDate\":\"1992-03-02T00:00:00\",\"IsYoungDriver\":true}],\"Car\":[{\"Id\":1,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":176664996},{\"Id\":2,\"Make\":\"BMW\",\"Model\":\"F25\",\"TravelledDistance\":476132712},{\"Id\":3,\"Make\":\"BMW\",\"Model\":\"M5 F10\",\"TravelledDistance\":140799461},{\"Id\":4,\"Make\":\"BMW\",\"Model\":\"F04\",\"TravelledDistance\":418839575},{\"Id\":5,\"Make\":\"BMW\",\"Model\":\"E88\",\"TravelledDistance\":27453411},{\"Id\":6,\"Make\":\"BMW\",\"Model\":\"M Roadster E85\",\"TravelledDistance\":475685374},{\"Id\":7,\"Make\":\"BMW\",\"Model\":\"1M Coupe\",\"TravelledDistance\":39826890},{\"Id\":8,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":401291910},{\"Id\":9,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":322292247},{\"Id\":10,\"Make\":\"BMW\",\"Model\":\"M6 E63\",\"TravelledDistance\":57189891},{\"Id\":11,\"Make\":\"BMW\",\"Model\":\"M6 E63\",\"TravelledDistance\":69863204},{\"Id\":12,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":487897422},{\"Id\":13,\"Make\":\"BMW\",\"Model\":\"F04\",\"TravelledDistance\":443756363},{\"Id\":14,\"Make\":\"BMW\",\"Model\":\"M5 F10\",\"TravelledDistance\":435603343},{\"Id\":15,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":227443571},{\"Id\":16,\"Make\":\"BMW\",\"Model\":\"E67\",\"TravelledDistance\":476830509},{\"Id\":17,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":277250812},{\"Id\":18,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":267253567},{\"Id\":19,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":254808828},{\"Id\":20,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":516628215},{\"Id\":21,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":156191509},{\"Id\":22,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":347259126},{\"Id\":23,\"Make\":\"Opel\",\"Model\":\"Kadet\",\"TravelledDistance\":31737446},{\"Id\":24,\"Make\":\"Opel\",\"Model\":\"Vectra\",\"TravelledDistance\":238042093},{\"Id\":25,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":225253817},{\"Id\":26,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":31468479},{\"Id\":27,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":282499542},{\"Id\":28,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":111313670},{\"Id\":29,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":168539389},{\"Id\":30,\"Make\":\"Opel\",\"Model\":\"Vectra\",\"TravelledDistance\":433351992},{\"Id\":31,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":349837452},{\"Id\":32,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":109910837},{\"Id\":33,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":241891505},{\"Id\":34,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":339785118},{\"Id\":35,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":284809463},{\"Id\":36,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":183346013}]}";

        var datasets = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JObject>>>(datasetsJson);

        foreach (var dataset in datasets)
        {
            var entityType = GetType(dataset.Key);
            var entities = dataset.Value
                .Select(j => j.ToObject(entityType))
                .ToArray();

            context.AddRange(entities);
        }

        context.SaveChanges();
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
        var services = new ServiceCollection();

        services
            .AddDbContext<TContext>(
                options => options
                    .UseInMemoryDatabase(databaseName)
                    .UseLazyLoadingProxies(useLazyLoading)
            );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}