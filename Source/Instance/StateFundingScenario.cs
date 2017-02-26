using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateFunding
{
  
[KSPScenario(ScenarioCreationOptions.AddToAllGames, new GameScenes[] {
		GameScenes.SPACECENTER,
		GameScenes.EDITOR,
		GameScenes.FLIGHT,
		GameScenes.TRACKSTATION,
	})
]  
  public class StateFundingScenario : ScenarioModule
  {
    private static StateFundingScenario _instance;
    public static StateFundingScenario Instance {
      get {
        return _instance;
      }
    }
    
    public ReviewManager ReviewMgr;
    public InstanceData data;
    public InstanceData Data { get { return data; } }
    public bool isInit;
    private const string CONFIG_NODENAME = "STATEFUNDINGSCENARIO";
    
    
    public StateFundingScenario () {
      	_instance = this;
    }
    

  	public override void OnAwake ()
  	{
    	if (data == null)
    	  data = new InstanceData();
        
    	if (ReviewMgr == null)
    	ReviewMgr = new ReviewManager();	
  	}    
    
    
    public void OnDestroy ()
  	{
      _instance = null;
      data = null;
      ReviewMgr = null;
  	} 
    
    
    //load scenario
    public override void OnLoad (ConfigNode node) {
      //try {
        if (node.HasNode(CONFIG_NODENAME)) {
          //load
          Log.Info("StateFundingScenario loading from persistence");
          ConfigNode loadNode = node.GetNode(CONFIG_NODENAME);
          ConfigNode.LoadObjectFromConfig(data, loadNode);
          isInit = true;
          StateFundingGlobal.needsDataInit = false;
        }
        else {
          Log.Info("StateFundingScenario default init");
          //default init
          //var NewView = new NewInstanceConfigView ();
          StateFundingGlobal.needsDataInit = true;
          //NewView.OnCreate ((InstanceData Inst) => {
          //  data = Inst;
          //  ReviewMgr.CompleteReview ();
          //});
          //isInit = true;
        }

      //}
      //catch {
        
      //}
    }
    
    
    //save scenario
    public override void OnSave (ConfigNode node) {
      if (!isInit)
        return;
     
      Log.Info("StateFundingScenario saving to persistence");
      ConfigNode saveNode = ConfigNode.CreateConfigFromObject(data);
      node.SetNode(CONFIG_NODENAME, saveNode, true);
    }
    
    
  }
}

