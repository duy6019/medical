using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Bravure.Infrastructure.Services
{
    public class HumanizerMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (IsTransformRequired(context.PropertyAttributes))
            {
                context.DisplayMetadata.DisplayName = () => context.Key.Name.Humanize();
            }
        }

        private static bool IsTransformRequired(IReadOnlyList<object> propertyAttributes)
        {
            if (propertyAttributes == null)
                return false;

            if (propertyAttributes.OfType<DisplayNameAttribute>().Any())
                return false;

            if (propertyAttributes.OfType<DisplayAttribute>().Any())
                return false;

            return true;
        }
    }
}
