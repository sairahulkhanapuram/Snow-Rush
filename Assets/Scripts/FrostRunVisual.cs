using UnityEngine;
public class FrostRunVisual : MonoBehaviour
{
    private Transform model, leftLeg, rightLeg, leftArm, rightArm;
    private Material jacket, dark, pants, skin, visor, trim;
    private float phase;
    private void Start()
    {
        FrostRunGame game = FrostRunGame.Instance;
        if (game == null) return;
        jacket = game.MakeMaterial(new Color(.92f,.16f,.08f));
        dark = game.MakeMaterial(new Color(.035f,.08f,.13f));
        pants = game.MakeMaterial(new Color(.06f,.16f,.28f));
        skin = game.MakeMaterial(new Color(.78f,.55f,.39f));
        visor = game.MakeMaterial(new Color(.08f,.55f,.72f), .55f);
        trim = game.MakeMaterial(new Color(.96f,.98f,1f));
        model = new GameObject("Runner Visual Model").transform;
        model.SetParent(transform, false);
        Build(PrimitiveType.Capsule,"Winter Jacket",new Vector3(0,.05f,0),new Vector3(.62f,.55f,.38f),jacket);
        Build(PrimitiveType.Sphere,"Head",new Vector3(0,.78f,.02f),new Vector3(.34f,.36f,.34f),skin);
        Build(PrimitiveType.Sphere,"Blue Ski Goggles",new Vector3(0,.82f,.29f),new Vector3(.38f,.13f,.09f),visor);
        Build(PrimitiveType.Sphere,"Winter Beanie",new Vector3(0,1.02f,0),new Vector3(.38f,.18f,.38f),dark);
        Build(PrimitiveType.Sphere,"Beanie Pom",new Vector3(0,1.17f,0),new Vector3(.11f,.11f,.11f),trim);
        Build(PrimitiveType.Cube,"Backpack",new Vector3(0,.12f,-.27f),new Vector3(.42f,.48f,.2f),dark);
        leftLeg = Build(PrimitiveType.Capsule,"Left Leg",new Vector3(-.19f,-.58f,0),new Vector3(.2f,.43f,.22f),pants);
        rightLeg = Build(PrimitiveType.Capsule,"Right Leg",new Vector3(.19f,-.58f,0),new Vector3(.2f,.43f,.22f),pants);
        Build(PrimitiveType.Cube,"Left Boot",new Vector3(-.19f,-.91f,.08f),new Vector3(.24f,.16f,.36f),dark);
        Build(PrimitiveType.Cube,"Right Boot",new Vector3(.19f,-.91f,.08f),new Vector3(.24f,.16f,.36f),dark);
        leftArm = Build(PrimitiveType.Capsule,"Left Arm",new Vector3(-.42f,.08f,0),new Vector3(.19f,.43f,.19f),jacket);
        rightArm = Build(PrimitiveType.Capsule,"Right Arm",new Vector3(.42f,.08f,0),new Vector3(.19f,.43f,.19f),jacket);
    }
    private Transform Build(PrimitiveType type,string n,Vector3 pos,Vector3 scale,Material mat)
    {
        GameObject part = GameObject.CreatePrimitive(type);
        part.name=n; part.transform.SetParent(model,false);
        part.transform.localPosition=pos; part.transform.localScale=scale;
        part.GetComponent<Renderer>().material=mat;
        Collider c=part.GetComponent<Collider>(); if(c!=null) Destroy(c);
        return part.transform;
    }
    private void Update()
    {
        FrostRunGame game=FrostRunGame.Instance;
        if(model==null || game==null) return;
        if(game.IsPlaying && !game.IsPaused && !game.IsGameOver)
        {
            phase += Time.deltaTime*12f;
            float swing=Mathf.Sin(phase)*28f;
            leftLeg.localRotation=Quaternion.Euler(swing,0,0);
            rightLeg.localRotation=Quaternion.Euler(-swing,0,0);
            leftArm.localRotation=Quaternion.Euler(-swing,0,0);
            rightArm.localRotation=Quaternion.Euler(swing,0,0);
            model.localPosition=new Vector3(0,Mathf.Abs(Mathf.Sin(phase*2f))*.035f,0);
        }
        else
        {
            leftLeg.localRotation=rightLeg.localRotation=Quaternion.identity;
            leftArm.localRotation=rightArm.localRotation=Quaternion.identity;
        }
    }
}
