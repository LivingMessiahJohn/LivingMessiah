var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LivingMessiah>("livingmessiah");

builder.AddProject<Projects.LivingMessiahAdmin>("livingmessiahadmin");

builder.AddProject<Projects.LivingMessiahSukkot>("livingmessiahsukkot");

builder.Build().Run();
