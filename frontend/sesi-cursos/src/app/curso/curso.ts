import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { CursoService } from '../services/curso.service';
import { Curso as CursoModel, Modulo } from '../models/curso';   // apelido para não conflitar
import { Depoimento } from '../depoimento/depoimento';
import { FaleConosco } from '../fale-conosco/fale-conosco';

@Component({
  selector: 'app-curso',
  imports: [RouterLink, DatePipe, Depoimento, FaleConosco],
  templateUrl: './curso.html',
  styleUrl: './curso.css',
})
export class Curso implements OnInit {
  private route = inject(ActivatedRoute);
  private cursoService = inject(CursoService);

  curso = signal<CursoModel | null>(null);
  modulos = signal<Modulo[]>([]);
  erro = signal('');

  // Conteúdo da seção "Aulas online e avaliações"
  aulasOnline: string[] = [
    'Aulas gravadas disponíveis na plataforma, para assistir no seu ritmo.',
    'Encontros ao vivo com o professor para tirar dúvidas e aprofundar o conteúdo.',
    'Atividades práticas ao final de cada módulo.',
    'Avaliação final para a emissão do certificado.',
  ];

  // Conteúdo da seção "Dúvidas frequentes"
  duvidas: { pergunta: string; resposta: string }[] = [
    {
      pergunta: 'Como faço para me matricular?',
      resposta: 'Clique no botão "Matricule-se" e siga as instruções para concluir a sua inscrição.',
    },
    {
      pergunta: 'O curso emite certificado?',
      resposta: 'Sim. Ao concluir o curso e ser aprovado nas avaliações, você recebe o certificado SESI.',
    },
    {
      pergunta: 'Por quanto tempo tenho acesso ao conteúdo?',
      resposta: 'O acesso fica disponível durante todo o período do curso.',
    },
    {
      pergunta: 'Posso estudar no meu próprio horário?',
      resposta: 'Sim. As aulas online ficam disponíveis a qualquer momento, e os encontros ao vivo têm datas divulgadas com antecedência.',
    },
  ];

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.cursoService.obter(id).subscribe({
      next: (c) => this.curso.set(c),
      error: () => this.erro.set('Curso não encontrado.'),
    });

    this.cursoService.modulos(id).subscribe({
      next: (m) => this.modulos.set(m),
      error: () => this.modulos.set([]),   // sem módulos: a página mostra a mensagem padrão
    });
  }
}