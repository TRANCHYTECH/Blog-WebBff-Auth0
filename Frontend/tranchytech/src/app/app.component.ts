import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { map, Observable } from 'rxjs';
import { QuestionCategoriesGQL, QuestionCategoriesQuery } from './queries.generated';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [RouterModule, CommonModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'tranchytech';

  categories: Observable<NonNullable<QuestionCategoriesQuery['questionCategories']>["nodes"]>

  private questionCategoriesGQL = inject(QuestionCategoriesGQL)
  
  constructor() {
    this.categories = this.questionCategoriesGQL.watch().valueChanges.pipe(map(result => result.data.questionCategories?.nodes))
  }
}
