using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [Space]
    [Tooltip("달리기 / 상호작용 버튼")]
    private IRunAndInteractionButton RunAndInteractionButton;

    [SerializeField]
    private AttackButton attackButton;
    
    private Player player; 
    
    private void Start()
    {
        RunAndInteractionButton = FindObjectOfType<RunAndInteractionButton>();

        player = FindObjectOfType<Player>();

        attackButton.OnMouseEnter += player.TryAttack;
    }
}