import { TestBed } from '@angular/core/testing';
import { AuthFacade } from './auth-facade.service';

describe('AuthFacadeService', () => {
  let service: AuthFacade;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthFacade);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
