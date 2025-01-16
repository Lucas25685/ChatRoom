import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainAccountComponent } from './main-account.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MainAccountComponent', () => {
  let component: MainAccountComponent;
  let fixture: ComponentFixture<MainAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MainAccountComponent
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
