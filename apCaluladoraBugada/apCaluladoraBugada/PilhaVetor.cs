using System;
using System.Collections.Generic;
public class PilhaVetor<Dado> : IStack<Dado>
{
  const int MAXIMO = 1000;  // máximo default
  int topo;
  Dado[] area;
  int maximo;  // quantidade de posições efetivamente alocadas

  public int Tamanho => topo + 1;

  public bool EstaVazia => topo < 0;

  public PilhaVetor(int tamanhoDesejado)
  {
    topo   = -1;      // para esvaziar;
    area   = new Dado[tamanhoDesejado];
    maximo = tamanhoDesejado;
  }
  public PilhaVetor() : this(MAXIMO)  // Construtor default
  {
  }

  public void Empilhar(Dado umDado)
  {
    if (topo >= maximo-1)
      throw new PilhaCheiaException("Overflow de pilha!");

    area[++topo] = umDado;
  }

  public Dado Desempilhar()
  {
    if (topo < 0)
      throw new PilhaVaziaException("Underflow de pilha!");

    Dado desempilhado = area[topo];
    area[topo--] = default(Dado);  // libera o espaço de memória não mais usado
    return desempilhado;
  }

  public Dado OTopo()
  {
    if (topo < 0)
      throw new PilhaVaziaException("Underflow de pilha!");

    return area[topo];
  }

  public List<Dado> Conteudo()
  {
    List<Dado> resultado = new List<Dado>();

    for (int indice= topo; indice >= 0; indice--)
      resultado.Add(area[indice]);

    return resultado;
  }

  public List<Dado> Conteudo(bool semIndices)
  {
    List<Dado> resultado = new List<Dado>();

    PilhaVetor<Dado> auxiliar = new PilhaVetor<Dado>(maximo);
    while (!this.EstaVazia)
    {
      resultado.Add(this.OTopo());
      auxiliar.Empilhar(this.Desempilhar());
    }

    while (!auxiliar.EstaVazia)
      this.Empilhar(auxiliar.Desempilhar());

    return resultado;
  }
}

