//using BlazorPWA.Components.Components;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.RenderTree;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace BlazorPWA.Components.Shared
//{
//    public class Grid<T> : ComponentBase
//    {
//        [Parameter]
//        private IReadOnlyList<T> Source { get; set; }
//        [Parameter]
//        private RenderFragment Header { get; set; }
//        [Parameter]
//        private RenderFragment Footer { get; set; }
//        [Parameter]
//        private RenderFragment<RenderFragment> Column { get; set; }
//        [Parameter]
//        private RenderFragment<PropertyInfo> ColumnCell { get; set; }
//        [Parameter]
//        private RenderFragment<RenderFragment<T>> Row { get; set; }
//        [Parameter]
//        private RenderFragment<object> RowCell { get; set; }

//        protected override void BuildRenderTree(RenderTreeBuilder builder)
//        {
//            base.BuildRenderTree(builder);

//            builder.AddContent(0, Header);

//            builder.AddContent(1, typeof(T).GetProperties().ToRenderFrament(Column, ColumnCell));


//            builder.AddContent(2, Source.ToRenderFrament(Row, RowCell));

//            builder.AddContent(0, Footer);
//        }
//    }
//}
