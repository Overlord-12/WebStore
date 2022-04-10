using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStore.Infrastructure.Conventions;

public class AddAreasControllerRoute : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var type_namespace = controller.ControllerType.Namespace;
        if (string.IsNullOrEmpty(type_namespace)) return;

        const string areas_namespace_suffix = "Areas.";
        const int areas_namespace_suffix_length = 6;

        var areas_index = type_namespace.IndexOf(areas_namespace_suffix, StringComparison.OrdinalIgnoreCase);
        if (areas_index < 0) return;

        areas_index += areas_namespace_suffix_length;
        var area_name = type_namespace[areas_index..type_namespace.IndexOf('.', areas_index)];

        if (string.IsNullOrEmpty(area_name)) return;

        if (controller.Attributes.OfType<AreaAttribute>().Any(a => a.RouteKey == "area" && a.RouteValue == area_name)) return;

        controller.RouteValues["area"] = area_name;
    }
}
