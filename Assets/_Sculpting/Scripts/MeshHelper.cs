using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class MeshHelper {

	    public HashSet<int> Connection { get; }

	    private MeshHelper() {
		    Connection = new HashSet<int>();
	    }

	    private void Connect (int to) {
		    Connection.Add(to);
	    }

	    public static Dictionary<int, MeshHelper> BuildNetwork (int[] triangles) {
		    var table = new Dictionary<int, MeshHelper>();

		    for(int i = 0, n = triangles.Length; i < n; i += 3) {
			    int a = triangles[i], b = triangles[i + 1], c = triangles[i + 2];
			    if(!table.ContainsKey(a)) {
				    table.Add(a, new MeshHelper());
			    }
			    if(!table.ContainsKey(b)) {
				    table.Add(b, new MeshHelper());
			    }
			    if(!table.ContainsKey(c)) {
				    table.Add(c, new MeshHelper());
			    }
			    table[a].Connect(b); 
			    table[a].Connect(c);
			    table[b].Connect(a); 
			    table[b].Connect(c);
			    table[c].Connect(a);
			    table[c].Connect(b);
		    }

		    return table;
	    }

    }
}