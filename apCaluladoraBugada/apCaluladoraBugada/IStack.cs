using System;
using System.Collections.Generic;
public interface IStack<Dado> // Dado indica que trataremos classes genéricas na pilha
{
  void Empilhar(Dado umDado); // empilha umDado, que é da classe genérica Dado
  Dado Desempilhar();         // desempilha e retorna o elemento da classe Dado
  Dado OTopo();
  int Tamanho { get; }
  bool EstaVazia { get; }
  List<Dado> Conteudo();
}