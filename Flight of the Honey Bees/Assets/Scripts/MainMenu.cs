using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	Sprite emptySprite;
	[SerializeField]
	GameObject trivia;
	[SerializeField]
	GameObject titleScreen;
	[SerializeField]
	GameObject infoScreen;
	[SerializeField]
	GameObject credits;
	[SerializeField]
	GameObject disclaimer;
	[SerializeField]
	Text infoText;
	[SerializeField]
	Text statsText;
	[SerializeField]
	Image spriteImage;
	[SerializeField]
	Image realImage;
	[SerializeField]
	Image secondRealImage;
	[SerializeField]
	Sprite spriteMasonBee;
	[SerializeField]
	Sprite realMasonBee;
	[SerializeField]
	Sprite spriteEUHoneyBee;
	[SerializeField]
	Sprite realEUHoneyBee;
	[SerializeField]
	Sprite spriteGiantHoney;
	[SerializeField]
	Sprite realGiantHoney;
	[SerializeField]
	Sprite spriteCarpenter;
	[SerializeField]
	Sprite realCarpenter;
	[SerializeField]
	Sprite spriteKiller;
	[SerializeField]
	Sprite realKiller;
	[SerializeField]
	Sprite spriteJapaneseGiant;
	[SerializeField]
	Sprite realJapaneseGiant;
	[SerializeField]
	Sprite realJapaneseGiant2;
	[SerializeField]
	Sprite spriteRedSpiderLily;
	[SerializeField]
	Sprite realRedSpiderLily;
	[SerializeField]
	Sprite realRedSpiderLily2;
	[SerializeField]
	Sprite spriteBirch;
	[SerializeField]
	Sprite realBirch;
	[SerializeField]
	Sprite spriteAxolotl;
	[SerializeField]
	Sprite realAxolotl;
	[SerializeField]
	Sprite realAxolotl2;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		SceneManager.LoadScene ("Level1");
	}

	public void QuitGame() {
		// save any game data here
		#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void ShowTitleScreen() {
		titleScreen.SetActive (true);
		trivia.SetActive (false);
	}

	public void ShowTrivia() {
		titleScreen.SetActive (false);
		trivia.SetActive (true);
	}

	public void ShowInfo(string species) {
		trivia.SetActive (false);
		infoScreen.SetActive (true);
		string temp;
		string stats;
		switch (species) {
		case "NAMason":
			temp = "North American Mason Bee\nScientific Name:  Osmia lignaria.\nMason bees get their name from their use of mud and other types of “masonry” products to construct their nests.\nThese bees are solitary with every female having its own nest.\nAs a solitary be, the Mason bee does not produce any honey or beeswax, but are among the top species used in agriculture for pollination.\nThey are easily to cultivate, and are docile in nature--rarely stinging when handled.\n";
			stats = "Health: 3\nSpeed: 4\nAttack: Mud Projectiles\n";
			spriteImage.sprite = spriteMasonBee;
			realImage.sprite = realMasonBee;
			secondRealImage.sprite = emptySprite;
			break;
		case "EUHoney":
			temp = "European Honeybee\nScientific Name: Apis mellifera.\nCommonly known as the Western Honey Bee.\nThese bees are the most common species of honeybee worldwide and are found on every continent except Antarctica.\nThese bees are highly social and are considered “superorganisms” because the colony is the significant biological unit and reproduce through swarming.\nUnlike others, the honeybees are also perennial and with colonies persisting \n";
			stats = "Health: 2\nSpeed: 5\n";
			spriteImage.sprite = spriteEUHoneyBee;
			realImage.sprite = realEUHoneyBee;
			secondRealImage.sprite = emptySprite;
			break;
		case "GiantHoney":
			temp = "Southeast Asian Giant Honey Bee\nScientific Name: Apis dorsata.\nFound in the forests of Nepal, Singapore, and India.\nThese social bees are known for their defensive strategies when disturbed and have a “waggle dance” to communicate food source locations to other bees in the same colony.\nColonies of this species are typically aggregated, but have little interconnection between each other.\nThey exhibit multiple defense strategies when threatened such as “heat balling” and “shimmering” where bees signal other bees to repeat the same upwards movement to create a wave, confusing wasps. \n";
			stats = "Health: 4\nSpeed: 3\n";
			spriteImage.sprite = spriteGiantHoney;
			realImage.sprite = realGiantHoney;
			secondRealImage.sprite = emptySprite;
			break;
		case "SonoranCarpenter":
			temp = "Sonoran Carpenter Bee\nScientific Name: Xylocopa sonorina.\nOriginated from eastern Pacific Islands.\nFemales are larger in size [than males] and black in color, but are considered shy in comparison to males that are golden brown in color and lack stingers.\n";
			stats = "Health: 3\nSpeed: 4\nAttack: Wood Chips\n";
			spriteImage.sprite = spriteCarpenter;
			realImage.sprite = realCarpenter;
			secondRealImage.sprite = emptySprite;
			break;
		case "AfricanizedHoney":
			temp = "Africanized Honey Bee (Killer Bee)\nHybrid of Apis mellifera ligustica (Italian Bee) and Apis mellifera iberiensis (Spanish Bee).\nConsidered an invasive species in North America.\nEvolved without proper beekeeping and in hard environments. As a result, they are a highly defensive species unfit for commercial or domestic use.\nKnown to pursue perceived threats over 1640 ft.\nWidely feared by public misinformation. Their sting retains the same potency as European Honey Bees. Their dangerousness comes from their easy provocation and their determination in pursuit.\nSome populations are gentler and are used for beekeeping in South America.\n";
			stats = "Health: ∞\nSpeed: 6\n";
			spriteImage.sprite = spriteKiller;
			realImage.sprite = realKiller;
			secondRealImage.sprite = emptySprite;
			break;
		case "JapaneseGiant":
			temp = "Japanese Giant Hornet\nScientific Name: Vespa mandarinia japonica.\nSubspecies of the world's largest hornet native to Japan.\nCan grow up to 4.5 cm with a wingspan greater than 6 cm.\nIts 6.25 mm long stinger injects venom that attacks the nervous system and damages tissue.\nPredate on European Honey Bees to feed their larvae. Only known species with defense are Japanese Honey Bees, which form a tight swarm with temperatures increasing up ~45°C around the hornet known as 'heat balling.'\n";
			stats = "Health: ∞\nSpeed: 3\nAttack: Venom\n";
			spriteImage.sprite = spriteJapaneseGiant;
			realImage.sprite = realJapaneseGiant;
			secondRealImage.sprite = realJapaneseGiant2;
			break;
		case "SpiderLily":
			temp = "Red Spider Lily\nScientific Name: Lycoris radiata.\nOriginally from China, Korea, and Nepal, but was introduced to Japan and then the United States.\nFlowers grow in irregular shape and pattern with characteristics of narrow petals that curve backwards with long stamens.\nHeavily attributed to Chinese and Japanese lore that these flowers are commonly associated with the dead and are meant to guide the dead in the afterlife.\nKnown as 彼岸花 'Higanbana' in Japan, named after the other side of the Sanzu River (similar to the River Styx in Greek Mythology).\n";
			stats = "";
			spriteImage.sprite = spriteRedSpiderLily;
			realImage.sprite = realRedSpiderLily;
			secondRealImage.sprite = realRedSpiderLily2;
			break;
		case "WhiteBirch":
			temp = "White Birch\nScientific Name: Betula papyrifera.\nAlso known commonly as “Paper Birch” or “Canoe Birch”\nNative to northern North America.\nOlder birch trees have white bark characteristic black horizontal stripes and dark green leaves. These turn bright yellow in Autumn.\nDuring seeding years, 1 million seeds/acre are produced and up to 35 million in bumper years.\n";
			stats = "";
			spriteImage.sprite = spriteBirch;
			realImage.sprite = realBirch;
			secondRealImage.sprite = emptySprite;
			break;
		case "Axolotl":
			temp = "Axolotl\nScientific Name: Ambystoma mexicanum.\nKnown as the “Mexican Walking Fish” or Tiger Salamander.\nOrigins of the species include Lake Xochimilco, Mexico City.\nModel specimen for research on limb regeneration. However, this ability has significantly reduced.\nBreathe cutaneously though skin and have external lungs when waters are anoxic (low O2).\nIUCN Red List: Critically Endangered.\n";
			stats = "Health: ∞\nDamage: 1\n";
			spriteImage.sprite = spriteAxolotl;
			realImage.sprite = realAxolotl;
			secondRealImage.sprite = realAxolotl2;
			break;
		default:
			temp = "None";
			stats = "None";
			break;
		};
		infoText.text = temp;
		statsText.text = stats;
	}

	public void BackToTrivia() {
		trivia.SetActive (true);
		infoScreen.SetActive (false);
	}

	public void DisclaimerToMenu() {
		disclaimer.SetActive (false);
		titleScreen.SetActive (true);
	}

	public void MenuToDisclaimer() {
		disclaimer.SetActive (true);
		titleScreen.SetActive (false);
	}

	public void CreditsToMenu() {
		credits.SetActive (false);
		titleScreen.SetActive (true);
	}

	public void MenuToCredits() {
		credits.SetActive (true);
		titleScreen.SetActive (false);
	}
}
