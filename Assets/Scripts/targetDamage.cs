using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class targetDamage : MonoBehaviour//Life
{
    public GameObject targetPoint;
//    public Material blue;
//    public Material red;
//    public MeshRenderer mesh;
//    public GameObject text;
//    public Transform model;
//    public bool inDamage;
//    public GameObject player;

//    private void Awake()
//    {
//        mesh = GetComponentInChildren<MeshRenderer>();
//        model = mesh.transform;
//    }
//    public override void GetHit(int d)
//    {
//        Debug.Log("Entró a Damage");
//        if (inDamage)
//            return;
//        base.GetHit(d);
//        inDamage = true;
//        mesh.material = red;
//        model.DOShakePosition(1f, 1,10,90,false,true,ShakeRandomnessMode.Full).OnComplete(() => model.localPosition = Vector3.zero);
//        GameObject t = Instantiate(text, UIManager.Instance.transform);
//        t.GetComponent<Text>().text = d.ToString();
//        t.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
//        float y = t.GetComponent<RectTransform>().position.y;
//        t.GetComponent<RectTransform>().DOMoveY(y + 250f, 1f).OnComplete(() => Destroy(t));
//        t.GetComponent<Text>().DOFade(0, 1f);
//        Time.timeScale = 0;
//        DG.Tweening.Sequence time = DOTween.Sequence();
//        time.AppendInterval(.2f).OnComplete(() => Time.timeScale = 1).SetUpdate(true);
//        DG.Tweening.Sequence s = DOTween.Sequence();
//        s.AppendInterval(1f).OnComplete(() =>
//        {
//            inDamage = false;
//            if (currentLife > 0)
//            {
//                mesh.material = blue;
//            }
//            else
//            {
//                this.enabled = false;
//                Destroy(gameObject, .2f);
//            }
//        });

//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//            other.GetComponent<Life>().GetHit(25);
//    }
//    private void OnDestroy()
//    {
//        if(player != null){
//            player.GetComponent<PlayerMotion>().noTarget();
//            player = null;
//        }


//    }
}
