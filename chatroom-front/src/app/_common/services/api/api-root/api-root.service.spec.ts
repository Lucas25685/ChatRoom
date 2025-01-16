import { TestBed } from '@angular/core/testing';

import { ApiRootService } from './api-root.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ApiRootService', () => {
  let service: ApiRootService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
    service = TestBed.inject(ApiRootService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
