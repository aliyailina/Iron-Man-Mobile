using System.Collections.Generic;
using Android.Content;
using Android.Renderscripts;
using Android.Views;
using Android.Widget;

namespace IronMan_mobile2
{
    public class ScriptsAdapter : BaseAdapter<string>
    {
            private List<string> list;
            private Context context;

            public ScriptsAdapter(Context context, List<string> list)
            {
                this.list = list;
                this.context = context;
            }

            public override int Count => list.Count;

            public override string this[int position] => list[position];

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.script, null, false);
                    TextView scriptName = view.FindViewById<TextView>(Resource.Id.textView5);
                    scriptName.Text = list[position];
                }

                return view;
            }
    }
}