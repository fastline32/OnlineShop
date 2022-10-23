import { TestBed } from '@angular/core/testing';

import { RuleGuardsGuard } from './rule-guards.guard';

describe('RuleGuardsGuard', () => {
  let guard: RuleGuardsGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RuleGuardsGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
