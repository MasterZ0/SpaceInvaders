using System.Collections.Generic;

public static class ExtensionMethods {
    public static int DrawIndex(this IList<float> chances) {  // This can be improved
        // Chances[0] = .01
        // Chances[1] = .10 
        // Chances[2] = .10
        // Chances[3] = .13
        List<float> interval = new List<float>();
        float range = 0;
        for (int i = 0; i < chances.Count; i++) {
            range += chances[i];
            interval.Add(range);
        }
        // Interval[0] = .01
        // Interval[1] = .11 
        // Interval[2] = .21
        // Interval[3] = .44


        if (interval.Count > 0) {
            float item = UnityEngine.Random.Range(0, range); // 0 to .44

            for (int i = 0; i < interval.Count; i++) {
                //I .2 <= .01 [0] (FALSE), 
                //I .2 <= .11 [1] (FALSE),
                //I .2 <= .21 [2] (TRUE)
                if (item <= interval[i]) {
                    return i;
                }
            }
        }

        throw new System.Exception();
    }
}
