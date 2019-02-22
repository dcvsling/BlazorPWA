using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorPWA.Components.Components
{

    internal static class RenderFragmentHelper
    {
        public static RenderFragment ToRenderFrament<T>(
            this IEnumerable<T> seq,
            RenderFragment<RenderFragment> bodyTemplate,
            RenderFragment<T> itemTemplate)
            => seq.ToRenderFrament(0, bodyTemplate, itemTemplate);

        public static RenderFragment ToRenderFrament<T>(
            this IEnumerable<T> seq,
            RenderFragment<T> itemTemplate)
            => seq.ToRenderFrament(0, itemTemplate);

        public static RenderFragment ToRenderFrament<T>(
            this IEnumerable<T> seq,
            int numb,
            RenderFragment<T> itemTemplate)
            => builder => builder.AddContent(
                numb,
                seq.RenderArray(itemTemplate));

        public static RenderFragment ToRenderFrament<T>(
            this IEnumerable<T> seq,
            int numb,
            RenderFragment<RenderFragment> bodyTemplate,
            RenderFragment<T> itemTemplate)
            => builder => builder.AddContent(
                numb,
                bodyTemplate,
                seq.RenderArray(itemTemplate));

        private static RenderFragment RenderArray<T>(this IEnumerable<T> seq, RenderFragment<T> itemTemlpate)
            => sub => seq
                .Aggregate(sub, Render(itemTemlpate));

        private static Func<RenderTreeBuilder, T, RenderTreeBuilder> Render<T>(RenderFragment<T> itemTemlpate)
            => (b, t) =>
            {
                itemTemlpate(t).Invoke(b);
                return b;
            };

        //public static RenderFragment ToRenderFrament<T>(
        //    this IEnumerable<T> seq,
        //    int numb,
        //    RenderFragment<RenderFragment<T>> bodyTemplate,
        //    RenderFragment<object> itemTemplate)
        //{
        //    var properties = typeof(T).GetProperties();
        //    var data = seq.Select(x => properties.Select(y => x.GetValue(y)).ToArray()).ToArray();
        //    return builder => builder.AddContent(
        //         numb,
        //         render => data.Select((x, i) => (x, i))
        //             .Aggregate(
        //                 render,
        //                 (r, s) => s.x.Aggregate(r,  r.AddContent(s.i, itemTemplate(s.x)))
        //         );
        //}

        //private static object GetValue<T>(this T t, PropertyInfo property)
        //    => property.GetValue(t);
    }
}
