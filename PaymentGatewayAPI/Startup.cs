using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace PaymentGatewayAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IStoneTransactionService, StoneTransactionService>();
            services.AddScoped<ICieloTransactionService, CieloTransactionService>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API de Gateway de pagamentos",
                    Description = "API que faz a integração de várias prestadoras de serviço de pagamentos (Adquirentes) e torna as requisições unificadas.",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Igor Muniz", Email = "munizig@hotmail.com", Url = "https://github.com/munizig" }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });



            //services.AddSwaggerGen(options =>
            //{
            //    string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    string moduleName = GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml");
            //    string filePath = Path.Combine(basePath, moduleName);
            //    string readme = File.ReadAllText(Path.Combine(basePath, "README.md"));

            //    //ApiKeyScheme scheme = Configuration.GetSection("ApiKeyScheme").Get<ApiKeyScheme>();
            //    //options.AddSecurityDefinition("Authentication", scheme);

            //    Info info = Configuration.GetSection("Info").Get<Info>();
            //    info.Description = readme;
            //    options.SwaggerDoc(info.Version, info);

            //    options.IncludeXmlComments(filePath);
            //    options.DescribeAllEnumsAsStrings();
            //    //options.OperationFilter<ExamplesOperationFilter>();
            //    //options.DocumentFilter<HideInDocsFilter>();
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvcWithDefaultRoute();
            app.UseMvc();

            //UseSwagger — Deve ser utilizado para expor a documentação gerada pelo Swagger 
            //como um endpoint json.
            //app.UseSwagger(x => x.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.ToString()));
            app.UseStaticFiles();
            app.UseSwagger();

            //UseStaticFiles - Deve ser utilizado para que o arquivo redoc.min.js fique disponível 
            //como um arquivo estático depois de gerado. 
            //app.UseStaticFiles();

            //if (env.IsDevelopment())
            //{
            //UseSwaggerUI — Deve ser utilizado somente em ambiente de desenvolvimento, 
            //pois ele expõe a interface interativa do swagger, onde é possível realizar 
            //testes de response e request para a aplicação.
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGatewayAPI");
                x.InjectStylesheet("/swagger/ui/custom.css");
            });
            //}

        }

    }
}
