// BitGarden intelec property, trust on me please

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PathNode e o equivalente ao no do grafo, so que de uma forma em que o inimigo possa caminhar por ele

// Lista de coisas que precisam pro pathfindin
// Dentro de EnemyMovement pegar o startPoint e mandar para a funcao generatePath(A*)
// Fica "olhando" a quantidade de entidades (sempre q diminuir se aumentar n muda) no jogo e se mudar chamar a funcao denovo
// A* ?
// Precisa: grafo(nos e se sao andaveis ou n), initial point, funcao de custo (entropia de manhattan), vetor com os caminhos
// metodos findPath, getNearestEntity
// Array de posicoes com o caminho gerado pelo a*
// End point

public class PathNode
{
    public int gCost { get; set; } // Custo ate chegar nesse no
    public int hCost { get; set; } // Custo da heristica de manhattan
    public int fCost { get; set; }
    public PathNode cameFromNode;
    public BgTile bgTile;
    public int x;
    public int y;

    public PathNode(BgTile bgTile)
    {
        this.bgTile = bgTile;
        this.x = bgTile.Position.X;
        this.y = bgTile.Position.Y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost; // g = 9999999, h = 
    }

    public void SetIsWalkable(bool isWalkable)
    {
        bgTile.IsWalkable = isWalkable;
    }

    public override string ToString()
    {
        return bgTile.Position.ToString();
    }

}
