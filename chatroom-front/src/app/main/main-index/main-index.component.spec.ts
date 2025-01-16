import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainIndexComponent } from './main-index.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MainIndexComponent', () => {
  let component: MainIndexComponent;
  let fixture: ComponentFixture<MainIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MainIndexComponent,
        HttpClientTestingModule
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render message', () => {
    const fixture = TestBed.createComponent(MainIndexComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Response: ');
  });
});
