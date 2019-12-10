import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../person';
import { environment } from '../../environments/environment'
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  private readonly _API = `${environment.API}Person`;

  constructor(private http: HttpClient) { }

  list() {
    return this.http.get<Person[]>(this._API);
  }

  getById(id) {
    return this.http.get<Person>(`${this._API}/${id}`)
      .pipe(take(1));
  }

  private create(person) {
    return this.http.post(this._API, person)
      .pipe(take(1));
  }

  private update(id, person) {
    return this.http.put(`${this._API}/${id}`, person)
      .pipe(take(1));
  }

  save(id, person) {
    if (id !== null)
      return this.update(id, person);
    else
      return this.create(person);
  }

  remove(id) {
    return this.http.delete(`${this._API}/${id}`)
      .pipe(take(1));
  }
}
