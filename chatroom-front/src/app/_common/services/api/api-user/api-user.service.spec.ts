import { TestBed } from '@angular/core/testing';

import { ApiUserService } from './api-user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ApiUserService', () => {
  let service: ApiUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ApiUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
