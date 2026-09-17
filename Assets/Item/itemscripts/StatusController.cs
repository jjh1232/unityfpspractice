using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
	
	[SerializeField]
	public int hp;
    public int Maxhp;
	[SerializeField]
	public int cp;
    public int Maxcp;
	[SerializeField]
	public int bullettan;
    [SerializeField]
    private PlayerCtrl Player;
	[SerializeField]
    private Image[] images_Gauge;
	[SerializeField]
	public int shield;
	static public bool SH = false;
    private const int HP = 0, CP = 1 ;
	public Text HPText;
	public Text BulletText;

    public float stoptimer;
    public float waittime;
    public float stoptimer1;
    public float waittime1;
    // Start is called before the first frame update
    void Start()
    {
		HPText.text = hp.ToString() +"/100";
		BulletText.text = bullettan.ToString();

        stoptimer1 = 0;
        waittime1= 5;
        stoptimer = 0;
        waittime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        GaugeUpdate();

		if(SH==true){
		stoptimer1 += Time.deltaTime;
		if (waittime1 < stoptimer1)
            {
                SH = false;
				shield = 0;
				stoptimer1 = 0;
            }
            if (shield == 0)
            {
                SH = false;
                stoptimer1 = 0;
            }
		}
        if (ItemEffectDatabase.MSTOP)
        {
            stoptimer += Time.deltaTime;

            if (waittime < stoptimer)
            {
                ItemEffectDatabase.MSTOP = false;
            }
        }
    }
	private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)hp / Maxhp;
        images_Gauge[CP].fillAmount = (float)cp / Maxcp;
        
    }
	 public void IncreaseHP(int _count)
    {
        
          if (hp + _count < Maxcp)
		{
            hp += _count;
			HPText.text = hp.ToString() +"/"+Maxhp.ToString();
		
			}
        else
            hp = Maxhp;
			HPText.text = hp.ToString() +"/"+Maxhp.ToString();
		
		

    }
    public void IncreaseCP(int _count)
    {
        if (cp + _count < Maxcp)
		{
            cp += _count;
			}
        else
            cp = Maxcp;
    }
	public void bulletin(int _count)
	{
		
			bullettan += _count;
			
		BulletText.text = bullettan.ToString();
	
	}
	public void DecreaseHP(int _count)
	{
		if(SH==true){
			shield -=_count;
		}
		if(SH==false){
			hp -=_count;
			HPText.text = hp.ToString() +"/100";
			}
	}
	public void Shield()
	{	
		SH=true;
		shield = 30;
		
            
            
        
	}
}
